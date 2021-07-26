import sys
from pdfminer.pdfdocument import PDFDocument
from pdfminer.pdfparser import PDFParser
from pdfminer.pdfinterp import PDFResourceManager, PDFPageInterpreter
from pdfminer.pdfdevice import PDFDevice, TagExtractor
from pdfminer.pdfpage import PDFPage
from pdfminer.converter import XMLConverter, HTMLConverter, TextConverter
from pdfminer.cmapdb import CMapDB
from pdfminer.layout import LAParams
from pdfminer.image import ImageWriter
from datetime import datetime
import calendar
import re
from dateutil.relativedelta import relativedelta
from keywords import level, keys
from flair.data import Sentence
from flair.models import SequenceTagger
tagger = SequenceTagger.load("flair/ner-english-large")
def parse_date(x, fmts=("%b %Y", "%B %Y")):
    for fmt in fmts:
        try:
            return datetime.strptime(x, fmt)
        except ValueError:
            pass

def extract_work_experience(text):
    months = "|".join(calendar.month_abbr[1:] + calendar.month_name[1:])
    pattern = fr"(?i)((?:{months}) *\d{{4}}) *(?:-|–) *(present|(?:{months}) *\d{{4}})"
    total_experience = None

    for start, end in re.findall(pattern, text):
        if end.lower() == "present":
            today = datetime.today()
            end = f"{calendar.month_abbr[today.month]} {today.year}"

        duration = relativedelta(parse_date(end), parse_date(start))

        if total_experience:
            total_experience += duration
        else: 
            total_experience = duration

        print(f"{start}-{end} ({duration.years} years, {duration.months} months)")

    if total_experience:
        # print(f"total experience:  {total_experience.years} years, {total_experience.months} months")
        return str(total_experience.years) + " years " + str(total_experience.months) + " months "
    else:
        return None

def export_name(text):
    sentence = Sentence(text)
    tagger.predict(sentence)
    for entity in sentence.get_spans("ner"):
        if entity.tag == "PER":
            return entity.text
    return None


def compare_keywords(position=None, language=None, level_pass=None, text=None):
    print(position, language)
    keywords = keys[position][language]
    total_point = 0
    points = 0
    for key, point in keywords.items():
        total_point += point
        if len(re.findall(key.lower(), text)):
            points += point
    accuracy = int(points*100//total_point)
    if accuracy in level.get(level_pass) or accuracy > level.get(level_pass)[-1]:
        return accuracy, True    
        
    return accuracy, None

def extract_phone(text):
    ext = text.split('\n')
    for _s in ext:
        if len(_s) > 9:
            list_p = _s.split(' ')
            _s = "".join(list_p)
            phone = _s[-10:]
            if phone.startswith("0") and len(phone)==10 and phone.isdigit():
                return phone
    return None

def extract_mail(text):
    ext = text.split('\n')
    for _s in ext:
        if '@' in _s:
            mail = _s.split(" ")[-1]
            return mail
    return None

def process_pdf(fp):
    caching = True
    outfp = sys.stdout
    laparams = LAParams()
    imagewriter = None
    pagenos = set()
    maxpages = 0
    password = b''
    rotation = 0
    encoding = 'utf-8'
    rsrcmgr = PDFResourceManager(caching=caching)
    device = TextConverter(rsrcmgr, outfp, laparams=laparams,
                                imagewriter=imagewriter)

    # with open(file_path, "rb") as fp:
    interpreter = PDFPageInterpreter(rsrcmgr, device)
    for page in PDFPage.get_pages(fp, pagenos, maxpages=maxpages,password= password,caching= caching, check_extractable=True):
        page.rotate = (page.rotate+rotation) % 360
        interpreter.process_page(page)
    normal_text = device.fulltext
    full_text = device.fulltext.lower()
    device.close()
    return full_text, normal_text

