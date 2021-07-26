package com.ncc.SaoDoBE.entity;

import javax.persistence.*;
import java.util.List;
import java.util.UUID;

@Entity
@Table(name = "user_role")
public class UserRole {
    @Id
    @Column(name = "id", insertable = false, updatable = false)
    @GeneratedValue
    private UUID id;
    @Column(name = "name", nullable = false)
    private String name;
    @OneToMany(mappedBy = "userGroup", cascade = {CascadeType.ALL}, fetch = FetchType.EAGER)
    private List<UserGroupRole> groupRoles;

    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<UserGroupRole> getGroupRoles() {
        return groupRoles;
    }

    public void setGroupRoles(List<UserGroupRole> groupRoles) {
        this.groupRoles = groupRoles;
    }
}
