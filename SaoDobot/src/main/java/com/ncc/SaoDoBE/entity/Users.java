package com.ncc.SaoDoBE.entity;

import com.ncc.SaoDoBE.domain.UsersDTO;

import javax.persistence.*;
import java.time.LocalDateTime;
import java.util.UUID;

@Entity
@Table(name = "users")
public class Users {

    @Id
    @Column(name = "id", insertable = false, updatable = false)
    @GeneratedValue
    private UUID id;
    @Column(name = "email", nullable = false)
    private String email;
    @Column(name = "password", nullable = false)
    private String password;
    @Column(name = "created_date", nullable = false)
    private LocalDateTime createdDate;
    @Column(name = "last_updated")
    private LocalDateTime lastUpdated;
    @Column(name = "last_login")
    private LocalDateTime lastLogin;
    @OneToOne(cascade = CascadeType.PERSIST)
    @JoinColumn(name = "employee_id", nullable=false)
    private Employee employee;
    @OneToOne(cascade = CascadeType.PERSIST)
    @JoinColumn(name = "user_group_id", nullable = false)
    private UserGroup userGroup;

    public Users() {
    }

    public Users(String email, String password, Employee employee, UserGroup userGroup) {
        this.id = UUID.randomUUID();
        this.email = email;
        this.password = password;
        this.createdDate = LocalDateTime.now();
        this.employee = employee;
        this.userGroup = userGroup;
    }

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

    public Employee getEmployee() {
        return employee;
    }

    public void setEmployee(Employee employee) {
        this.employee = employee;
    }

    public UserGroup getUserGroup() {
        return userGroup;
    }

    public void setUserGroup(UserGroup userGroup) {
        this.userGroup = userGroup;
    }

    public UsersDTO toDTO() {
        UsersDTO.Builder builder = new UsersDTO.Builder()
                .withUsersId(id)
                .withEmail(email)
                .withPassword(password)
                .withCreatedDate(createdDate)
                .withLastUpdated(lastUpdated)
                .withLastLogin(lastLogin)
                .withEmployee(employee.getId())
                .withUserGroup(userGroup.getId());
        UsersDTO dto = builder.build();
        return dto;
    }
}
