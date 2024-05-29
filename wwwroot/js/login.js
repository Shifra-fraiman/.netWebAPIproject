const uri = "/Login";
const loginFrom = document.getElementById('login-from');

const Login = async () => {
    await fetch(uri, {
        method: 'POST',
        body: JSON.stringify(
            {
                "Name": loginFrom.name.value,
                "Password": loginFrom.password.value
            }
        ),
        headers: {
            'Content-Type': 'application/json',
        },
    })
        .then(res => {
            console.log("res: "+res);
            if (!res.ok) {
                console.log("res: " + res.ok + res);
                throw new Error('Login-faild!')
            }
            else
                return res.text();
        })
        .then(token => {
            console.log("I accept token: " + token);
            localStorage.setItem('token', token);
            window.location.href = './index.html';
        })
        .catch(error => {
            console.error("Login faild: " + error)
            alert('login faild')
        })
};

