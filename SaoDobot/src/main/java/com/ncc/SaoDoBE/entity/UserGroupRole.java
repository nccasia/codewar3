package com.ncc.SaoDoBE.entity;

import javax.persistence.*;
import java.util.UUID;

@Entity
@Table(name = "user_group_role")
public class UserGroupRole {

    @Id
    @Column(name = "id", insertable = false, updatable = false)
    @GeneratedValue
    private UUID id;

    @ManyToOne
    @JoinColumn(name = "user_role_id")
    private UserRole userRole;

    @ManyToOne
    @JoinColumn(name = "user_group_id")
    private UserGroup userGroup;

    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public UserRole getUserRole() {
        return userRole;
    }

    public void setUserRole(UserRole userRole) {
        this.userRole = userRole;
    }

    public UserGroup getUserGroup() {
        return userGroup;
    }

    public void setUserGroup(UserGroup userGroup) {
        this.userGroup = userGroup;
    }
}
