const mainCheckBoxChangeHandler = (formClassName, checkboxName) => (sender) => {
    const elements = document.getElementsByClassName(formClassName);
    Array.prototype.slice.call(elements)
        .forEach(item => {
            item[checkboxName]['checked'] = sender["checked"];
        });
}

const handler = mainCheckBoxChangeHandler('user-item-form', 'checkbox');
const getChanged = (className, predicate = null) => {
    return Array.prototype.slice.call(document.getElementsByClassName(className))
        .reduce((r, i) => {
            if (i['checkbox'].checked) {
                if (predicate && !predicate(i.itemId.value)) {
                    return r;
                }
                r._.push(i.itemId.value);
            }
            return r;
        },
            { _: [] })._;
}


const deleteUrl = 'user/delete';
const lockUrl = 'user/lock';
const unlockUrl = 'user/unlock';

function createForm(method, url, textList) {
    const form = document.createElement('form');
    form.method = method;
    form.hidden = true;
    form.action = url;
    textList.reduce((f, x) => {
        const input = document.createElement('input');
        input.type = 'text';
        input.setAttribute('value', x);
        input.name = 'user-id';
        f.appendChild(input);
        return f;
    }, form);
    const btn = document.createElement('input');
    btn.name = 'submit';
    btn.type = 'submit';
    form.appendChild(btn);

    return form;
}

async function deleteAction() {
    const ids = getChanged('user-item-form');
    const form = createForm('POST', deleteUrl, ids);

    document.body.appendChild(form);
    form.submit.click();


}

const predicateMaker = status => x => document.getElementById(`${x}-status`).innerText === status;

function lockAction() {
    const ids = getChanged('user-item-form', predicateMaker('Active'));
    if (ids.length < 1) return;
    const form = createForm('POST', lockUrl, ids);

    document.body.appendChild(form);
    form.submit.click();
}

function unlockAction() {
    const ids = getChanged('user-item-form', predicateMaker('Blocked'));

    if (ids.length < 1) return;
    const form = createForm('POST', unlockUrl, ids);
    document.body.appendChild(form);

    form.submit.click();
}