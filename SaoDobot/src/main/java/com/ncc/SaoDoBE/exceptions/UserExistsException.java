package com.ncc.SaoDoBE.exceptions;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.client.RestClientException;

@ResponseStatus(code = HttpStatus.BAD_REQUEST, reason = "User have been exists")
public class UserExistsException extends RestClientException {
    public UserExistsException(String msg) {
        super(msg);
    }
}