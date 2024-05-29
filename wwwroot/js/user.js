const url = '/User';
let users = [];

document.addEventListener("DOMContentLoaded", function () { getUsers(); });

async function getUsers() {
    await fetch(url, {
        headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`
        }
    })
        .then(response => { console.log(`response: users: ${response}`); return response.json() })
        .then(data => _displayUsers(data))
        .catch(error => console.log('Unable to get items.'));
}

function _displayUsers(data) {
    document.getElementById("usersTable").style.display = "block";
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    _displayCount(data.length);
    const button = document.createElement('button');

    data.forEach(item => {
        let isAdmin = document.createElement('input');
        isAdmin.type = 'checkbox';
        isAdmin.disabled = true;
        isAdmin.checked = item.isAdmin;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteUser(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isAdmin);

        let td2 = tr.insertCell(1);
        let textNodeID = document.createTextNode(item.id);
        td2.appendChild(textNodeID);

        let td3 = tr.insertCell(2);
        let textNode = document.createTextNode(item.name);
        td3.appendChild(textNode);

        let td4 = tr.insertCell(3);
        let textNodePasswor = document.createTextNode(item.password);
        td4.appendChild(textNodePasswor);

        let td5 = tr.insertCell(4);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(5);
        td6.appendChild(deleteButton);
    });

    users = data;
    console.log("users: " + users);

}
function addUser() {
    const addNameTextbox = document.getElementById('add-name');
    const addPasswordTextbox = document.getElementById('add-password');
    const addIsAdminCheckbox = document.getElementById('add-isAdmin');

    console.log(`addIsAdminCheckbox: ${addIsAdminCheckbox.checked}`);
    const user = {
        Name: addNameTextbox.value.trim(),
        password: addPasswordTextbox.value.trim(),
        IsAdmin: addIsAdminCheckbox.checked,
    };

    fetch(url, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            Authorization: `Bearer ${localStorage.getItem("token")}`
        },
        body: JSON.stringify(user)
    })
        .then(response => response.json())
        .then(() => {
            getUsers();
            addNameTextbox.value = '';
            addPasswordTextbox.value = '';
            addIsAdminCheckbox.checked = false
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteUser(id) {
    fetch(`${url}/${id}`, {
        method: 'DELETE',
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
    },
    )
        .then(() => getUsers())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    debugger;
    const item = users.find(item => item.id === id);
    debugger;
    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-Id').value = item.id;
    document.getElementById('edit-isAdmin').checked = item.isAdmin;
    document.getElementById('edit-password').value = item.password;
    document.getElementById('editForm').style.display = 'block';
}

function updateUser() {
    const userID = document.getElementById('edit-Id').value;
    const user = {
        Id: parseInt(userID, 10),
        isAdmin: document.getElementById('edit-isAdmin').checked,
        name: document.getElementById('edit-name').value.trim(),
        password: document.getElementById('edit-password').value.trim()
    };
    console.log(`Id: ${user.Id} desc: ${user.name} is: ${user.isAdmin} userID: ${userID}`);

    fetch(`${url}/${userID}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
        body: JSON.stringify(user)
    })
        .then(() => getUsers())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'user' : 'users';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}
