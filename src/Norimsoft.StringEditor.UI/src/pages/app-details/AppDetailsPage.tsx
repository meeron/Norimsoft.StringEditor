import { useParams } from '@solidjs/router';

export default function AppDetailsPage() {
  const params = useParams();
  
  return <h1>This is {params.appSlug} page</h1>
}
