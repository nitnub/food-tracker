import { Link } from 'react-router-dom';

export default function PageNotFound() {
  const homePath = '/';
  return (
    <>
      <h1>Page Not Found</h1>
      Unable to find page.
      <br />Return <Link to={homePath}>home</Link>.
    </>
  );
}
