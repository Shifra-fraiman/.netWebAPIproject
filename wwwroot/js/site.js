const uri = '/taskList';
const buttonUser = document.createElement('button');
const editUser = document.getElementById("editUser");
function getItems() {
    fetch(uri, {
        headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`
        }
    })
        .then(response => { console.log(response); return response.json() })
        .then(data => { _displayItems(data); return data; })
        .then(data => _displayEditButtonUser(data))
        .catch(error => console.log('Unable to get items.'));
}
function _displayEditButtonUser(data) {
    buttonUser.setAttribute('onclick', `displayEditFormUser(${data[0].userId})`);
    buttonUser.innerHTML = "edit your details";
    editUser.appendChild(buttonUser);
}
function _displayItems(data) {
    console.log("hello _displayItems!");
    const tBody = document.getElementById('tasks');
    tBody.innerHTML = '';

    _displayCount(data.length);
    console.log("after _displayCount");
    const button = document.createElement('button');

    data.forEach(item => {
        let isGlutenFreeCheckbox = document.createElement('input');
        isGlutenFreeCheckbox.type = 'checkbox';
        isGlutenFreeCheckbox.disabled = true;
        isGlutenFreeCheckbox.checked = item.isDoing;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isGlutenFreeCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.description);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    tasks = data;

    console.log("tasks: " + tasks);

}
function addItem() {
    const addNameTextbox = document.getElementById('add-name');

    const item = {
        IsDoing: false,
        Description: addNameTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            Authorization: `Bearer ${localStorage.getItem("token")}`
        },

        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE',
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
    },
    )
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    debugger;
    const item = tasks.find(item => item.id === id);
    debugger;
    document.getElementById('edit-TaskName').value = item.description;
    document.getElementById('edit-Id').value = item.id;
    document.getElementById('edit-IsDone').checked = item.isDoing;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-Id').value;
    const item = {
        Id: parseInt(itemId, 10),
        IsDoing: document.getElementById('edit-IsDone').checked,
        Description: document.getElementById('edit-TaskName').value.trim()
    };
    console.log(`Id: ${item.Id} desc: ${item.Description} is: ${item.IsDoing}`);

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}


function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'task' : 'task kinds';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}


document.addEventListener("DOMContentLoaded", function () {
    if (localStorage.getItem("token")) {
        const token = localStorage.getItem("token");
        const tokenParts = token.split('.');
        if (tokenParts.length === 3) {
            const tokenPayload = JSON.parse(atob(tokenParts[1]));
            console.log(`tokenPayload: ${tokenPayload} tokenPayload.type: ${tokenPayload.type}`);
            if (tokenPayload.type == "User,Admin") {
                document.getElementById("usersButton").style.display = "block";
            }
        }
    }
});

async function displayEditFormUser(id) {
    debugger;
    let user;
    console.log(localStorage.getItem("token"));
    await fetch(`/User/${id}`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`
        }
    })
        .then(response => { console.log(`response: users: ${response}`); return response.json() })
        .then(data => user = data)
        .catch(error => console.log('Unable to get items.'));
    //const item = users.find(item => item.id === id);
    // console.log(`item: ${item} `);
    // console.log(`item.password ${item.password}`);
    debugger;
    document.getElementById('edit-Id-user').value = id;
    document.getElementById('edit-name').value = user.name;
    document.getElementById('edit-password').value = user.password;
    document.getElementById('editFormUser').style.display = 'block';
}

async function updateUser() {
    const userID = document.getElementById('edit-Id-user').value;
    const user = {
        Id: parseInt(userID, 10),
        // isAdmin: document.getElementById('edit-isAdmin').checked,
        name: document.getElementById('edit-name').value.trim(),
        password: document.getElementById('edit-password').value.trim()
    };
    console.log(`Id: ${user.Id} desc: ${user.name} is: ${user.isAdmin} userID: ${userID}`);

    fetch(`User/${userID}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
        body: JSON.stringify(user)
    })
        .then(() => alert(`the user name is: ${user.name} and the password is: ${user.password}`))
        .then(() => {
            document.getElementById('edit-Id-user').value = '';
            document.getElementById('edit-name').value = '';
            document.getElementById('edit-password').value = '';
            document.getElementById('editFormUser').style.display = 'none';
        })
        .catch(error => console.error('Unable to update item.', error));

    await closeInputUser();

    return false;
}
function closeInputUser() {
    document.getElementById('editFormUser').style.display = 'none';
}

