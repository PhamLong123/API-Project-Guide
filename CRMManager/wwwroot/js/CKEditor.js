ClassicEditor
    .create(document.querySelector('#editor'), {

        licenseKey: '',
        removePlugins: [
            'CodeBlock'
        ]
    })

    .then(editor => {
        window.editor = editor;
    })
    .catch(error => {
        console.error('Oops, something went wrong!');
        console.error('Please, report the following error on https://github.com/ckeditor/ckeditor5/issues with the build id and the error stack trace:');
        console.warn('Build id: havu80dolzy3-npk6ns3a04tg');
        console.error(error);
    });


