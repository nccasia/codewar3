package com.ncc.SaoDoBE.entity;

import com.ncc.SaoDoBE.enums.UserGroupEnum;

import javax.persistence.*;
import java.util.List;
import java.util.UUID;

@Entity
@Table(name = "user_group")
public class UserGroup {
    @Id
    @Column(name = "id", insertable = false, updatable = false)
    @GeneratedValue
    private UUID id;
    @Column(name = "name", nullable = false)
    @Enumerated(EnumType.STRING)
    private UserGroupEnum name;
    @OneToMany(mappedBy = "userGroup", cascade = {CascadeType.ALL}, fetch = FetchType.EAGER)
    private List<UserGroupRole> groupRoles;

    @OneToOne(mappedBy = "userGroup", cascade = CascadeType.ALL)
    private Users user;

    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public UserGroupEnum getName() {
        return name;
    }

    public void setName(UserGroupEnum name) {
        this.name = name;
    }

    public Users getUser() {
        return user;
    }

    public void setUser(Users user) {
        this.user = user;
    }

    public List<UserGroupRole> getGroupRoles() {
        return groupRoles;
    }

    public void setGroupRoles(List<UserGroupRole> groupRoles) {
        this.groupRoles = groupRoles;
    }
}
