const uri = "Login/Login";
const loginFrom = document.getElementById('login-from');

const Login = async () => {
    const postReponse = await fetch(uri, {
        method: 'POST',
        body: JSON.stringify(
            {
                "Name": loginFrom.name,
                "Password": loginFrom.password
            }
        ),
        headers: {
            'Content-Type': 'application/json',
        },
    })
        .then(res => {
            if (!res.Ok) {
                throw new Error('Login-faild!')
            }
            else
                return res.text();
        })
        .then(token => {
            localStorage.setItem('token', token)
            window.location.href('./js/site.js');
        })
        .catch(error => {
            console.error("Login faild: " + error)
            alert('login faild')
        })
};



