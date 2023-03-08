// eslint-disable-next-line @typescript-eslint/no-unused-vars
import * as React from 'react';
import { HubConnectionBuilder, HttpTransportType, HubConnection } from '@microsoft/signalr';

async function fetchBlog(url: string, setData: any) {
  try {
    const res = await fetch(url);
    const data = await res.json();
    setData(data);
  } catch (err) {
    alert(err);
  }
}
export function App() {
  const [blogData, setBlogData] = React.useState(null);
  const [connection, setConnection] = React.useState<HubConnection | null>(null);
  const url = `http://bs-blog.sample/api/v1/blog/blogs`;
  const blogUrl = 'http://bs-blog.sample:80/hub/v1/blog'; //http:/bs-blog.sample

  React.useEffect(() => {
    // const url = process.env.NX_API_URL || '';


    fetchBlog(url, setBlogData);
  }, [url]);

  React.useEffect(() => {

    const newConnection = new HubConnectionBuilder()
      .withUrl(blogUrl, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .build();

    newConnection.start().catch(err => {
      console.log(err);
    });
    setConnection(newConnection);
    newConnection.on("ReceiveAllMessage", async () => {
      fetchBlog(url, setBlogData);
    });

    return () => {
      newConnection.stop();
    }

  }, [url]);

  return (
    <div>
      <pre>
        {blogData && JSON.stringify(blogData, null, 2)}
      </pre>
    </div>
  );
}

export default App;
