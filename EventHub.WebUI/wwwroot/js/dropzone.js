const dropzoneDiv = document.getElementById('dropzone-div');
const imageInput = document.getElementById('dropzone-file');
const uploadedImage = document.getElementById('uploadedImage');
var imageName = "";
const updateImageName = document.getElementById('updateImageName');
const deleteImageUrl = document.getElementById('deleteImage');
const imageButtons = document.getElementById('imageButtons');

imageInput.addEventListener('change', async () => {
    const formData = new FormData();
    formData.append('image', imageInput.files[0]);

    try {
        const response = await fetch('/image/upload', {
            method: 'POST',
            body: formData,
        });

        if (response.ok) {
            const data = await response.text();
            uploadedImage.src = `/image/get/${data}`;
            updateImageName.value = data;
            deleteImageUrl.href += `/${data}`;
            imageName = data;
        } else {
            console.error('Upload failed.');
        }
        dropzoneDiv.classList.add('hidden');
        uploadedImage.classList.remove('hidden');
        imageButtons.classList.remove('hidden');
        deleteImageUrl.removeAttribute('disabled');
    } catch (error) {
        console.error('An error occurred:', error);
    }
});

deleteImageUrl.addEventListener('click', async () => {
    try {
        const response = await fetch(`/image/delete/${imageName}`, {
            method: 'POST',
        });

        if (!response.ok){
            console.error('Upload failed.');
        }
        deleteImageUrl.setAttribute('disabled', 'disabled');
        dropzoneDiv.classList.remove('hidden');
        uploadedImage.classList.add('hidden');
        imageButtons.classList.add('hidden');
    } catch (error) {
        console.error('An error occurred:', error);
    }
});