const emailRegexp = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

const isRequired = (val: string | null) => val && String(val).length > 0 || 'Обязательное поле';
const isEmailValid = (val: string | null) => val && emailRegexp.test(val) || 'Неверный формат email';
const onlyNumber = (val: any) => typeof Number(val) === 'number' && !isNaN(Number(val)) || 'Используйте только числа';

export {
    isRequired,
    isEmailValid,
    onlyNumber,
};
