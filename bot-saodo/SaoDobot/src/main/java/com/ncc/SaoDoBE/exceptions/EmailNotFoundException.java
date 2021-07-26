package com.ncc.SaoDoBE.exceptions;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.client.RestClientException;

@ResponseStatus(code = HttpStatus.NOT_FOUND, reason = "User not found")
public class EmailNotFoundException extends RestClientException {
    public EmailNotFoundException(String msg) {
        super(msg);
    }
}
