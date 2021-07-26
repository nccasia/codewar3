package com.ncc.SaoDoBE.domain;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.ncc.SaoDoBE.config.DTOBuilder;
import com.ncc.SaoDoBE.entity.Employee;
import com.ncc.SaoDoBE.entity.UserGroup;
import com.ncc.SaoDoBE.entity.Users;

import java.time.LocalDateTime;
import java.util.UUID;

public class UsersDTO {

    @JsonProperty
    private UUID id;
    @JsonProperty
    private String email;
    @JsonProperty
    private String password;
    @JsonProperty
    private LocalDateTime createdDate;
    @JsonProperty
    private LocalDateTime lastUpdated;
    @JsonProperty
    private LocalDateTime lastLogin;
    @JsonProperty
    private UUID employeeId;
    @JsonProperty
    private UUID userGroupId;

    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
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

    public LocalDateTime getLastLogin() {
        return lastLogin;
    }

    public void setLastLogin(LocalDateTime lastLogin) {
        this.lastLogin = lastLogin;
    }

    public UUID getEmployeeId() {
        return employeeId;
    }

    public void setEmployeeId(UUID employeeId) {
        this.employeeId = employeeId;
    }

    public UUID getUserGroupId() {
        return userGroupId;
    }

    public void setUserGroupId(UUID userGroupId) {
        this.userGroupId = userGroupId;
    }

    public static class Builder extends DTOBuilder<UsersDTO> {

        @Override
        protected UsersDTO createDTO() {
            return new UsersDTO();
        }

        public Builder withUsersId(UUID usersId) {
            dto.id = usersId;
            return this;
        }
        public Builder withEmail(String email) {
            dto.email = email;
            return this;
        }

        public Builder withPassword(String password) {
            dto.password = password;
            return this;
        }
        public Builder withCreatedDate(LocalDateTime createdDate) {
            dto.createdDate = createdDate;
            return this;
        }
        public Builder withLastUpdated(LocalDateTime lastUpdated) {
            dto.lastUpdated = lastUpdated;
            return this;
        }

        public Builder withLastLogin(LocalDateTime lastLogin) {
            dto.lastLogin = lastLogin;
            return this;
        }
        public Builder withEmployee(UUID employeeId) {
            dto.employeeId = employeeId;
            return this;
        }

        public Builder withUserGroup(UUID userGroupId) {
            dto.userGroupId = userGroupId;
            return this;
        }
    }
}
