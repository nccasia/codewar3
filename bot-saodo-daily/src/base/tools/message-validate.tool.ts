export const messageValidate = (message: string): boolean => {
  return message.toLowerCase().includes('#daily');
};
