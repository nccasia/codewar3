package com.ncc.SaoDoBE.service;

import com.ncc.SaoDoBE.domain.SentimentTextVerify;
import com.ncc.SaoDoBE.exceptions.SentimentAnalyticException;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

@Service
public class SentimentService {

    @Value("${saodo.sentiment-analytic.uri}")
    private String sentimentUri;

    public String SentimentAnalytic(String text) {
        RestTemplate restTemplate = new RestTemplate();
        try {
            SentimentTextVerify textVerify = new SentimentTextVerify();
            textVerify.setText(text);
            String result = restTemplate.postForObject(
                    sentimentUri + "/verify-text",
                    textVerify,
                    String.class);
            return result;
        } catch (Exception e) {
            throw new SentimentAnalyticException("Call api some error!!");
        }
    }
}
