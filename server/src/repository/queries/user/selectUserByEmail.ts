export default (email: string) => {
  return `
    SELECT * FROM app_user WHERE lower(email) = '${email.toLowerCase()}';
  `;
};
