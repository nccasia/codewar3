package com.ncc.SaoDoBE.service;

import com.ncc.SaoDoBE.entity.Users;
import com.ncc.SaoDoBE.repository.UserGroupRoleRepository;
import com.ncc.SaoDoBE.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

public class UserDetailServiceImpl implements UserDetailsService {

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private UserGroupRoleRepository groupRoleRepository;
    @Override
    public UserDetails loadUserByUsername(String username) {
        Optional<Users> user = userRepository.findByEmail(username);
        if (user.isPresent()) {
            throw new UsernameNotFoundException("username not found exception" + username);
        }
        List<GrantedAuthority> roles = groupRoleRepository.findByUserGroup(user.get().getUserGroup())
                .stream()
                .map(role -> (GrantedAuthority) () -> role.getUserRole().getName())
                .collect(Collectors.toList());
        return new User(user.get().getEmail(), user.get().getPassword(), roles);
    }
}
