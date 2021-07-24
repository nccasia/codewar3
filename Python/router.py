from flask import Flask, request, render_template, jsonify
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity
from vaderSentiment.vaderSentiment import SentimentIntensityAnalyzer
from nltk.corpus import stopwords
import nltk
import train

nltk.download('stopwords')

set(stopwords.words('english'))
app = Flask(__name__)

@app.route('/')
def get():
    return render_template('form.html')

@app.route('/', methods=['POST'])
def post():
    # stop_words = stopwords.words('english')
    text1 = request.form['text1'].lower()
    text1 = text1.strip()

    # processed_doc1 = ' '.join([word for word in text1.split() if word not in stop_words])

    # sa = SentimentIntensityAnalyzer()
    # dd = sa.polarity_scores(text=processed_doc1)
    # compound = round((1 + dd['compound'])/2, 2)

    data = train.TextClassificationPredict()
    pos = data.get_train_data(text1)['pos']
    percent = data.get_train_data(text1)['percent']
    if('POS' in pos):
        pos = 'POS'
    elif('NEG' in pos):
        pos = "NEG"
    else:
        pos = 'NEU'
    print(pos)

    return render_template('form.html', final=pos, percent=percent, text1=text1)

@app.route('/verify-text', methods=['POST'])
def verify_text():
    # stop_words = stopwords.words('english')
    data = request.json
    text = data['text']
    print(data)
    print(text)

    # processed_doc1 = ' '.join([word for word in text1.split() if word not in stop_words])

    # sa = SentimentIntensityAnalyzer()
    # dd = sa.polarity_scores(text=processed_doc1)
    # compound = round((1 + dd['compound'])/2, 2)

    data = train.TextClassificationPredict()
    pos = data.get_train_data(text)['pos']
    percent = data.get_train_data(text)['percent']
    if('POS' in pos):
        pos = 'POS'
    elif('NEG' in pos):
        pos = "NEG"
    else:
        pos = 'NEU'
    print(pos)

    return jsonify({ "pos": pos}) 

if __name__ == "__main__":
    app.run(debug=True, host="127.0.0.1", port=5001, threaded=True)
