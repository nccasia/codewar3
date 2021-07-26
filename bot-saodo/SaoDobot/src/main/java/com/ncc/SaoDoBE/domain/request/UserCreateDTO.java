package com.ncc.SaoDoBE.domain.request;

import com.fasterxml.jackson.annotation.JsonProperty;

public class UserCreateDTO {

    @JsonProperty
    private String email;
    @JsonProperty
    private String password;
    @JsonProperty
    private String firstName;
    @JsonProperty
    private String surname;

    public String getEmail() {
        return email;
    }

    public String getPassword() {
        return password;
    }

    public String getFirstName() {
        return firstName;
    }

    public String getSurname() {
        return surname;
    }
}
