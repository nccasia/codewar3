package com.ncc.SaoDoBE.repository;

import com.ncc.SaoDoBE.entity.UserGroup;
import com.ncc.SaoDoBE.enums.UserGroupEnum;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface UserGroupRepository extends JpaRepository<UserGroup, UUID> {

    UserGroup findUserGroupByName(UserGroupEnum name);
}
