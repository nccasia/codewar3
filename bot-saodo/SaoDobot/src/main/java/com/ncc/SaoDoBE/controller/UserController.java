package com.ncc.SaoDoBE.controller;

import com.ncc.SaoDoBE.domain.UsersDTO;
import com.ncc.SaoDoBE.domain.request.UserCreateDTO;
import com.ncc.SaoDoBE.entity.Users;
import com.ncc.SaoDoBE.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("/api/users")
public class UserController {

    @Autowired
    private UserService userService;

    @GetMapping
    public List<UsersDTO> getAllUsers() {
        return userService.findAllUser();
    }

    @GetMapping("/{id}")
    public UsersDTO getUserById(@PathVariable UUID id) {
        return userService.getUserById(id);
    }

    @GetMapping("/user-email")
    public UsersDTO getUserByEmail(@RequestBody String email) {
        return userService.getUserByEmail(email);
    }

    @PostMapping("/create-user")
    public UsersDTO createNewUser(@RequestBody UserCreateDTO userDTO) {
        return userService.createNewUsers(userDTO);
    }
}
