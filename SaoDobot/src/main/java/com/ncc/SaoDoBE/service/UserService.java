package com.ncc.SaoDoBE.service;

import com.ncc.SaoDoBE.domain.UsersDTO;
import com.ncc.SaoDoBE.domain.request.UserCreateDTO;
import com.ncc.SaoDoBE.entity.Employee;
import com.ncc.SaoDoBE.entity.UserGroup;
import com.ncc.SaoDoBE.entity.Users;
import com.ncc.SaoDoBE.enums.UserGroupEnum;
import com.ncc.SaoDoBE.exceptions.EmailNotFoundException;
import com.ncc.SaoDoBE.exceptions.UserExistsException;
import com.ncc.SaoDoBE.repository.EmployeeRepository;
import com.ncc.SaoDoBE.repository.UserGroupRepository;
import com.ncc.SaoDoBE.repository.UserRepository;
import com.ncc.SaoDoBE.exceptions.UserNotFoundException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.UUID;
import java.util.stream.Collectors;

@Service
public class UserService {

    @Autowired
    private UserRepository userRepo;

    @Autowired
    private UserGroupRepository groupRepo;

    @Autowired
    private EmployeeRepository employeeRepo;

    public UsersDTO getUserById(UUID id) {

        Optional<Users> optionalUser = userRepo.findById(id);
        if(!optionalUser.isPresent()) {
            throw new UserNotFoundException(id);
        }
        return userRepo.findById(id).get().toDTO();
    }

    public List<UsersDTO> findAllUser() {
        List<UsersDTO> usersDTOs = userRepo.findAll().stream()
                .map(user -> user.toDTO())
                .collect(Collectors.toList());
        return usersDTOs;
    }


    public UsersDTO getUserByEmail(String email) {

        Optional<Users> user = userRepo.findByEmail(email);
        if (!user.isPresent()) {
            throw new EmailNotFoundException("Could not found email : " + email);
        }
        return user.get().toDTO();
    }

    public UsersDTO createNewUsers(UserCreateDTO userCreaetDto) {
        Boolean check = employeeRepo.existsByEmail(userCreaetDto.getEmail());
        if(check) {
            throw  new UserExistsException("User have been exists !!!");
        }
        Employee employee = new Employee(userCreaetDto.getFirstName(), userCreaetDto.getSurname(), userCreaetDto.getEmail());
        employee = employeeRepo.save(employee);
        UserGroup userGroup= groupRepo.findUserGroupByName(UserGroupEnum.EMPLOYEE);
        Users user = new Users(userCreaetDto.getEmail(), userCreaetDto.getPassword(), employee, userGroup);
        return userRepo.save(user).toDTO();
    }

}
