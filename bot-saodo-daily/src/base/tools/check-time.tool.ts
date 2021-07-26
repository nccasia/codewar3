import moment from 'moment';

export const checkTimeIsInThisDayAndNow = (time: string): boolean => {
  const startThisDay = moment().set({ hour: 0, minute: 0, second: 0 }).toDate();
  return (
    moment(startThisDay).isBefore(moment(time)) &&
    moment(time).isAfter(moment())
  );
};

export const checkTimeIsBetweenTimeAndNow = (
  timeStart: string,
  timeToCheck: string,
): boolean => {
  return (
    moment(timeStart).isBefore(moment(timeToCheck)) &&
    moment(timeToCheck).isAfter(moment())
  );
};

export const checkTimeIsBetween2Time = (
  timeStart: string,
  timeToCheck: string,
  timeEnd: string,
): boolean => {
  return (
    moment(timeStart).isBefore(moment(timeToCheck)) &&
    moment(timeToCheck).isAfter(moment(timeEnd))
  );
};
