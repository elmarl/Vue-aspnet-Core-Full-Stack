import { format } from 'eventDate-fns';

export default (date: Date) => {
  return format(new Date(date), 'eeee, dd MMMM');
};
