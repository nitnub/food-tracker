export function isPGError(error: any) {
  const props = [
    'length',
    'name',
    'severity',
    'code',
    'detail',
    'hint',
    'position',
    'internalPosition',
    'internalQuery',
    'where',
    'schema',
    'table',
    'column',
    'dataType',
    'constraint',
    'file',
    'line',
    'routine',
  ];

  return isObjectWithProps(error, props);
}

export function isValidNewUser(user: UserDbEntry) {
  const props = ['globalUserId', 'email', 'admin', 'avatar'];

  return isObjectWithProps(user, props);
}

function isObjectWithProps(candidate: object, props: string[]) {
  const hasProps = props.every((p) => candidate.hasOwnProperty(p));
  const isCorrectLength = Object.keys(candidate).length === props.length;

  return isObject(candidate) && hasProps && isCorrectLength;
}

export function isObject(candidate: object) {
  return (
    typeof candidate === 'object' &&
    !Array.isArray(candidate) &&
    candidate !== null
  );
}
