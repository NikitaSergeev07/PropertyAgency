const emailRegexp = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

const isRequired = (val: string | null) => val && val.length > 0 || 'Обязательное поле';
const isEmailValid = (val: string | null) => val && emailRegexp.test(val) || 'Неверный формат email';

export {
    isRequired,
    isEmailValid,
};
