const uri = 'api/InterviewProcesses/';
let todos = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addCompanyNameTextbox = document.getElementById('add-company-name');
    const addJobNameTextbox = document.getElementById('add-job-name');
    const addDescriptionTextbox = document.getElementById('add-description');
    const addWhereDiscoveredTextbox = document.getElementById('add-where-discovered');
    const addReviewTextbox = document.getElementById('add-review');

    const item = {
        isComplete: false,
        companyName: addCompanyNameTextbox.value.trim(),
        jobName: addJobNameTextbox.value.trim(),
        description: addDescriptionTextbox.value.trim(),
        whereDiscovered: addWhereDiscoveredTextbox.value.trim(),
        review: addReviewTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addCompanyNameTextbox.value = '';
            addJobNameTextbox.value = '';
            addDescriptionTextbox.value = '';
            addWhereDiscoveredTextbox.value = '';
            addReviewTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = todos.find(item => item.id === id);

    document.getElementById('edit-company-name').value = item.companyName;
    document.getElementById('edit-job-name').value = item.jobName;
    document.getElementById('edit-description').value = item.description;
    document.getElementById('edit-where-discovered').value = item.whereDiscovered;
    document.getElementById('edit-review').value = item.review;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-isComplete').checked = item.isComplete;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: itemId,
        companyName: document.getElementById('edit-company-name').value.trim(),
        jobName: document.getElementById('edit-job-name').value.trim(),
        description: document.getElementById('edit-description').value.trim(),
        whereDiscovered: document.getElementById('edit-where-discovered').value.trim(),
        review: document.getElementById('edit-review').value.trim(),
        isComplete: document.getElementById('edit-isComplete').checked,
        applyDate: new Date().toLocaleString(),
        firstContact: null,
        endDate: null,
        interviewCount: 0,
        onlineAssesment: false,
        videoInterview: false,
        jobOffered: false,
        offerAccepted: false
    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    }).then(() => getItems()).catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'Interview process' : 'Interview processses';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('todos');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isCompleteCheckbox = document.createElement('input');
        isCompleteCheckbox.type = 'checkbox';
        isCompleteCheckbox.disabled = true;
        isCompleteCheckbox.checked = item.isComplete;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isCompleteCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.companyName);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        let textNode2 = document.createTextNode(item.jobName);
        td3.appendChild(textNode2);

        let td4 = tr.insertCell(3);
        let textNode3 = document.createTextNode(item.description);
        td4.appendChild(textNode3);

        let td5 = tr.insertCell(4);
        let textNode4 = document.createTextNode(item.whereDiscovered);
        td5.appendChild(textNode4);

        let td6 = tr.insertCell(5);
        let textNode5 = document.createTextNode(item.review);
        td6.appendChild(textNode5);

        let td7 = tr.insertCell(6);
        td7.appendChild(editButton);

        let td8 = tr.insertCell(7);
        td8.appendChild(deleteButton);

    });

    todos = data;
}