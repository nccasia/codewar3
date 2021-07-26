package com.ncc.SaoDoBE.repository;

import com.ncc.SaoDoBE.entity.Employee;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface EmployeeRepository extends JpaRepository<Employee, UUID> {

    Boolean existsByEmail(String email);
}
