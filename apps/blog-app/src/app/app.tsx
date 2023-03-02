// eslint-disable-next-line @typescript-eslint/no-unused-vars
import * as React from 'react';

export function App() {
  const [blogData, setBlogData] = React.useState(null);

  React.useEffect(() => {
    // const url = process.env.NX_API_URL || '';
    async function fetchBlog() {
      try {
        const res = await fetch(`http://bs-blog.sample/api/v1/blog/blogs`);
        const data = await res.json();
        setBlogData(data);
      } catch (err) {
        alert(err);
      }
    }

    fetchBlog();
  }, [])

  return (
    <div>
      <pre>
        {blogData && JSON.stringify(blogData, null, 2)}
      </pre>
    </div>
  );
}

export default App;
