import Sidebar from '@/components/Sidebar';
import { Layout } from 'antd';

export default function AssessmentLayout({children}: Readonly<{
    children: React.ReactNode;
  }>) {
return (
    <div className="wrapper" style={{height: '100vh', padding: '50px 100px', backgroundColor: '#101B2E'}}>
    <Layout style={{height: '100%', backgroundColor: 'rgb(16, 27, 46)', display: 'flex', gap: '20px', padding: '30px', color: '#fff', border: '1px solid #fff', borderRadius: '30px'}}>
        <Sidebar/>
        {children}
    </Layout>
    </div>
)
}