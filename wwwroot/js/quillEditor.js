const options = {
    theme: 'snow',
};
const editor = new Quill('#editor', options);
const form = document.querySelector('#form');
const description = document.querySelector('input[name=Description]');

editor.setContents(JSON.parse(description.value ? description.value : "{}"));

form.onsubmit = function () {
    description.value = JSON.stringify(editor.getContents());
};
