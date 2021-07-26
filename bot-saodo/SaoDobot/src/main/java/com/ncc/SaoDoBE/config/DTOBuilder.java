package com.ncc.SaoDoBE.config;

public abstract class DTOBuilder<T> {

    protected T dto;

    protected abstract T createDTO();

    public DTOBuilder(){
        dto = createDTO();
    }

    public T build(){
        return dto;
    }
}
