/**
 * Prints date in the same format as CSharp DateTime using InvariantCulture.
 * The format is "MM/dd/yyyy HH:mm".
 */
function toDatabaseKey(date) {
  let day = date.getDate();
  if (day < 10) day = `0${day}`;

  let month = date.getMonth() + 1;
  if (month < 10) month = `0${month}`;

  let hours = date.getHours();
  if (hours < 10) hours = `0${hours}`;

  let minutes = date.getMinutes();
  if (minutes < 10) minutes = `0${minutes}`;

  return `${month}/${day}/${date.getFullYear()} ${hours}:${minutes}`;
}

function toDatabaseKeyDate(date) {
  let day = date.getDate();
  if (day < 10) day = `0${day}`;

  let month = date.getMonth() + 1;
  if (month < 10) month = `0${month}`;

  return `${month}/${day}/${date.getFullYear()}`;
}

function formatTime(date) {
  const hours = date.getHours();
  const minutes = date.getMinutes();

  return `${hours < 10 ? `0${hours}` : hours}:${minutes < 10 ? `0${minutes}` : minutes}`;
}

function isSameHour(first, second) {
  return first.getHours() === second.getHours();
}

function isSameDay(date, planDate) {
  return date.getDate() === planDate.getDate() &&
    date.getMonth() === planDate.getMonth() &&
    date.getFullYear() === planDate.getFullYear();
}

export { toDatabaseKey, toDatabaseKeyDate, isSameHour, isSameDay, formatTime };