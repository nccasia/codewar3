#!/usr/bin/env python
# -*- coding: utf-8 -*-
import pandas as pd
from model import NaiveBayesModel

class TextClassificationPredict(object):
    def __init__(self):
        self.test = None

    def get_train_data(self, text):

        nRowsRead = 80000 # specify 'None' if want to read whole file
        # data - data.csv may have more rows in reality, but we are only loading/previewing the first 1000 rows
        df1 = pd.read_csv('data/data-data.csv', delimiter=',', nrows = nRowsRead)
        df1.dataframeName = 'data-data.csv'
        nRow, nCol = df1.shape
        print(f'There are {nRow} rows and {nCol} columns')

        # Tạo test data
        test_data = []
        test_data.append({"comment": text})
        df_test = pd.DataFrame(test_data)

        # init model naive bayes
        model = NaiveBayesModel()
        clf = model.clf.fit(df1["comment"], df1["label"])
        predicted = clf.predict(df_test["comment"])
        percent = clf.predict_proba(df_test["comment"])

        # Print predicted result
        d = dict()
        d['pos'] = predicted
        d['percent'] = percent
        print(predicted)
        print(percent)
        print(clf.predict_proba(df_test["comment"]))
        return d


if __name__ == '__main__':
    tcp = TextClassificationPredict()
    tcp.get_train_data()