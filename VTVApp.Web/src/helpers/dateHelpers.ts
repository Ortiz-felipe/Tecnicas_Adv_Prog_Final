import moment from 'moment';

export const formatDate = (dateString: string): string => {
  return moment.utc(dateString).local().format('DD/MM/YYYY')
};
