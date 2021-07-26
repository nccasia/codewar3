package com.ncc.SaoDoBE.exceptions;

import org.springframework.web.client.RestClientException;

public class SentimentAnalyticException extends RestClientException {
    public SentimentAnalyticException(String msg) {
        super(msg);
    }
}
