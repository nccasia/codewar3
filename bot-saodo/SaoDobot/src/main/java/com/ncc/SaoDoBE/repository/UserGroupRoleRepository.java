package com.ncc.SaoDoBE.repository;

import com.ncc.SaoDoBE.entity.UserGroup;
import com.ncc.SaoDoBE.entity.UserGroupRole;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.UUID;

public interface UserGroupRoleRepository extends JpaRepository<UserGroupRole, UUID> {
    List<UserGroupRole> findByUserGroup(UserGroup group);
}
