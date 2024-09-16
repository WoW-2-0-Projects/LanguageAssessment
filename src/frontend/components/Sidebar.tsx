"use client";

import { BookOutlined, MessageOutlined, SoundOutlined, CustomerServiceOutlined } from '@ant-design/icons';
import { Layout, Steps } from 'antd';
import Link from 'next/link';

const { Sider } = Layout;
const { Step } = Steps;

const Sidebar = () => {
  return (
    <>
      <Sider
        style={{
          background: 'rgb(16, 27, 46)'
        }}
      >
        <Steps
          direction="vertical"
          size="small"
          style={{
            borderRight: '1px solid #d9d9d9',
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'space-between',
            height: '100%',
            paddingRight: "30px"
          }}
          current={3}
        >
          <Step
            title={<Link style={{ color: '#4E019B' }} href="/assessment/introduction">Intorduction</Link>}
            icon={<MessageOutlined style={{ color: '#4E019B' }} />}
            description={<span style={{ color: '#4E019B' }}>Improve your English</span>}
          />
          <Step
            title={<Link style={{ color: '#5AFB7A' }} href="/assessment/grammar">Grammar</Link>}
            icon={<BookOutlined style={{ color: '#5AFB7A' }} />}
            description={<span style={{ color: '#5AFB7A' }}>Improve your grammar skills</span>}
          />
          <Step
            title={<Link style={{ color: '#3155FF' }} href="/assessment//listening">Listening</Link>}
            icon={<CustomerServiceOutlined style={{ color: '#3155FF' }} />}
            description={<span style={{ color: '#3155FF' }}>Enhance listening abilities</span>}
          />
          <Step
            title={<Link style={{ color: '#FF6FFF' }} href="/assessment//speaking">Speaking</Link>}
            icon={<SoundOutlined style={{ color: '#FF6FFF' }} />}
            description={<span style={{ color: '#FF6FFF' }}>Practice speaking exercises</span>}
          />
        </Steps>
      </Sider>
    </>
  );
};

export default Sidebar;
