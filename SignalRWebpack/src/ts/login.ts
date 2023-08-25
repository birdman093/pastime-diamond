import "../css/main.css";
document.addEventListener('DOMContentLoaded', function () {

    // Group Login 
    document.getElementById('loginForm').addEventListener('submit', function (event) {
        event.preventDefault();

        const groupId = (document.getElementById('groupID') as HTMLInputElement).value;
        const password = (document.getElementById('groupPassword') as HTMLInputElement).value;
        const username = (document.getElementById('username') as HTMLInputElement).value;

        if (groupId == "" || password == "" || username == "")
        {
            alert('Missing Field');
            return;
        }

        fetch('/check-login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ groupId, password, username }),
        })
            .then(response => {
                if (response.redirected) {
                    window.location.href = response.url;
                } else {
                    throw new Error('Login failed');
                }
            })
            .catch(error => {
                alert("Group: " + error.message);  // show an alert if the login failed
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
