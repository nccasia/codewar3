package com.ncc.SaoDoBE.controller;

import com.ncc.SaoDoBE.service.SentimentService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class SentimentController {

    @Autowired
    private SentimentService service;

    @PostMapping("/demo")
    public String sendrequest(@RequestBody String text) {
        return service.SentimentAnalytic(text);
    }
}
