const path = require('path');
const fs = require('fs');
const cors = require('cors');
const express = require('express');

const app = express();
const PORT = 3001;

app.use(cors({
    origin: '*',
    methods: 'GET,HEAD,PUT,PATCH,POST,DELETE',
    preflightContinue: false,
    optionsSuccessStatus: 204
}));

app.get('/', (req, res) => {
    res.send("<h2>Hello Wolrd!</h2>");
});

app.get('/uploads/:filename', (req, res) => {
    const filename = req.params.filename;
    const filepath = path.resolve(__dirname, './PropertyAgency/PropertyAgency.API/uploads', filename);

    fs.readFile(filepath, (err, data) => {
        if (err) {
            return res.send(`Файл – ${filename} не найден`);
        }

        // get image file extension name
        const extensionName = path.extname(filepath);

        // convert image file to base64-encoded string
        const base64Image = Buffer.from(data, 'binary').toString('base64');

        // combine all strings
        const base64ImageStr = `data:image/${extensionName.split('.').pop()};base64,${base64Image}`;

        res.send(base64ImageStr);
    });
});

app.listen(PORT, () => {
    console.log(`API is listening on port ${PORT}`);
});
