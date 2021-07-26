package com.ncc.SaoDoBE.exceptions;

import org.springframework.web.client.RestClientException;

import java.util.UUID;

public class UserNotFoundException extends RestClientException {
    public UserNotFoundException(UUID id) {
        super(String.format("User '%s' does not exist.", id));
    }
}
