document.addEventListener('DOMContentLoaded', function () {
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
                alert(error.message);  // show an alert if the login failed
            });
    });
});
