from calendar import month
from re import M
from flask import g,Flask,request,render_template,redirect,url_for, session,escape,jsonify, Response
import json
from utils import *
from keywords import keys
from flask_cors import CORS, cross_origin
from werkzeug.datastructures import ImmutableMultiDict 
app = Flask(__name__)
CORS(app, support_credentials=True)
@app.route('/', methods=["GET", "POST"])
@cross_origin(supports_credentials=True)
def index():
    if request.method == 'GET':
        results = []
        return jsonify(results)
    
    if request.method == "POST":
        requirements =  json.loads(request.form.to_dict(flat=False).get('info')[0])
        position = requirements.get("position")
        language = requirements.get("language")
        level_pass = requirements.get("level")
        if position != "Tester" and language == None:
            return "Please choose language", 400
        multifiles = request.files
        print(requirements)
        files = ImmutableMultiDict(multifiles)
        results = []
        for i, k in files.items():
            pdf_to_text, normal_text = process_pdf(k)
            work_experience = extract_work_experience(pdf_to_text)
            acc, passed = compare_keywords(position, language, level_pass, pdf_to_text)
            phone = extract_phone(pdf_to_text)
            mail = extract_mail(pdf_to_text)
            name = export_name(normal_text)
            results.append({
                "file": k.filename,
                "experiences": work_experience,
                "matching": acc,
                "pass": passed,
                "phone": phone,
                "mail": mail,
                "name": name
            })
        return jsonify(results)

if __name__ == "__main__":
    app.run(port=5001, host="0.0.0.0", debug=True)