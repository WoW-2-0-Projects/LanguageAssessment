"use client";
import ClientSession from '@/components/ClientSession';
import { Endpoints } from '@/constants';
import { Typography, Button } from 'antd';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';
const { Text } = Typography;

export default function IntroductionPage() {
    const router = useRouter()
    const [sessionReceived, setSessionReceived] = useState(false);
    useEffect(() => {
        async function getSession() {
            try {
                const response = await fetch(Endpoints.startSession, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "ngrok-skip-browser-warning": "true"
                    },
                });
                const data = await response.json();
                if (data) {
                    sessionStorage.setItem('ll__session', JSON.stringify(data))
                    setSessionReceived(true)
                }
            } catch (error) {
                console.error(error);
            }
        }
        getSession()
    }, [])
    return (
        <>
            <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', padding: '50px 100px', flexDirection: 'column', gap: "30px", }}>
                <Text style={{ maxWidth: '100%', color: '#fff', lineHeight: '1.4', fontWeight: 300, fontSize: '18px' }}>
                    Welcome to our comprehensive language assessment! This AI-powered test will evaluate your proficiency across three key areas: grammar, listening comprehension, and spoken language skills.
                    <br />
                    To begin, you will take a grammar test that assesses your knowledge of English language structures. This is a timed test with a single attempt allowed. Once you start, you must complete the grammar section within the allotted time.
                    <br />
                    Next, you will proceed to the listening comprehension test. This section will evaluate your ability to understand spoken English. You will have one chance to listen to audio and answer related questions. The time limit will be strictly enforced.
                    <br />
                    Finally, you will complete the spoken language test. This AI-powered assessment will analyze your pronunciation, fluency, and communication skills. You will be given a single opportunity to respond to prompts and engage in simulated conversations.
                    <br />
                    Your results will be calculated and displayed immediately after completing each test section. This allows you to gain valuable insights into your language abilities.
                    <br />
                    This assessment is created and conducted entirely by artificial intelligence. Our advanced algorithms are designed to provide an objective, reliable, and convenient evaluation of your English proficiency.

                    We wish you the best of luck as you embark on this language assessment journey! If you have any questions or concerns, please don't hesitate to contact our support team.
                </Text>
                <Button type="primary" disabled={!sessionReceived} onClick={() => sessionReceived ? router.push("/assessment/grammar") : ""} style={{ backgroundColor: '#FF977E', marginTop: "auto", padding: "20px 60px" }}>Start</Button>
            </div>
        </>

    )
}