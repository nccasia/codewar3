package com.ncc.SaoDoBE.domain;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.ncc.SaoDoBE.config.DTOBuilder;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.UUID;

public class EmployeeDTO {
    @JsonProperty
    private UUID id;
    @JsonProperty
    private String firstName;
    @JsonProperty
    private String surname;
    @JsonProperty
    private String email;
    @JsonProperty
    private LocalDateTime createdDate;
    @JsonProperty
    private LocalDateTime lastUpdated;
    @JsonProperty
    private LocalDate dateOfBirth;
    @JsonProperty
    private Boolean enabled;
    @JsonProperty
    private UUID userId;

    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getSurname() {
        return surname;
    }

    public void setSurname(String surname) {
        this.surname = surname;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public LocalDateTime getCreatedDate() {
        return createdDate;
    }

    public void setCreatedDate(LocalDateTime createdDate) {
        this.createdDate = createdDate;
    }

    public LocalDateTime getLastUpdated() {
        return lastUpdated;
    }

    public void setLastUpdated(LocalDateTime lastUpdated) {
        this.lastUpdated = lastUpdated;
    }

    public LocalDate getDateOfBirth() {
        return dateOfBirth;
    }

    public void setDateOfBirth(LocalDate dateOfBirth) {
        this.dateOfBirth = dateOfBirth;
    }

    public Boolean getEnabled() {
        return enabled;
    }

    public void setEnabled(Boolean enabled) {
        this.enabled = enabled;
    }

    public UUID getUserId() {
        return userId;
    }

    public void setUserId(UUID userId) {
        this.userId = userId;
    }

    public static class Builder extends DTOBuilder<EmployeeDTO> {

        @Override
        protected EmployeeDTO createDTO() {
            return new EmployeeDTO();
        }

        public EmployeeDTO.Builder withEmployeeId(UUID employeeId) {
            dto.id = employeeId;
            return this;
        }
        public EmployeeDTO.Builder withEmail(String email) {
            dto.email = email;
            return this;
        }

        public EmployeeDTO.Builder withFirstName(String firstName) {
            dto.firstName = firstName;
            return this;
        }
        public EmployeeDTO.Builder withSurname(String surname) {
            dto.surname = surname;
            return this;
        }
        public EmployeeDTO.Builder withLastUpdated(LocalDateTime lastUpdated) {
            dto.lastUpdated = lastUpdated;
            return this;
        }

        public EmployeeDTO.Builder withCreatedDate(LocalDateTime createdDate) {
            dto.createdDate = createdDate;
            return this;
        }
        public EmployeeDTO.Builder withDateOfBirth(LocalDate dateOfBirth) {
            dto.dateOfBirth = dateOfBirth;
            return this;
        }

        public EmployeeDTO.Builder withEnabled(Boolean enabled) {
            dto.enabled = enabled;
            return this;
        }

        public EmployeeDTO.Builder withUserId(UUID userId) {
            dto.userId = userId;
            return this;
        }
    }
}
