"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
require("../css/main.css");
document.addEventListener('DOMContentLoaded', function () {
    // Group Login 
    document.getElementById('loginForm').addEventListener('submit', function (event) {
        event.preventDefault();
        var groupId = document.getElementById('groupID').value;
        var password = document.getElementById('groupPassword').value;
        var username = document.getElementById('username').value;
        if (groupId == "" || password == "" || username == "") {
            alert('Missing Field');
            return;
        }
        fetch('/check-login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ groupId: groupId, password: password, username: username }),
        })
            .then(function (response) {
            if (response.redirected) {
                window.location.href = response.url;
            }
            else {
                throw new Error('Login failed');
            }
        })
            .catch(function (error) {
            alert("Group: " + error.message); // show an alert if the login failed
        });
    });
    // OAuth Login using Cookie Storage
    document.getElementById('userLogin').addEventListener('submit', function (event) {
        event.preventDefault();
        window.location.href = '/account';
    });
    // OAuth Logout using Cookie Storage
    document.getElementById('userLogout').addEventListener('submit', function (event) {
        event.preventDefault();
        window.location.href = '/logout';
    });
});
