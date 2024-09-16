'use client'
import { AssessmentProvider } from "@/context/assessment";
import { AssessmentTest } from "@/types/Assessment";
import Question from "./Question";
import QuestionControl from "./QuestionControl";
import QuestionCounter from "./QuestionCounter";
import { useEffect, useState } from "react";
import QuestionProgress from "./QuestionProgress";
import { Alert } from "antd";
import "./styles/_questionList.css"
import Loader from "./Loader";
import { useRouter } from "next/navigation";
import { Endpoints, TestTypes } from "@/constants";
import ListeningPage from "./ListeningPage";

export default function QuestionList({ testType }: { testType: string }) {
    const [assessment, setAssessment] = useState<AssessmentTest | null>();
    const [data, setData] = useState<AssessmentTest>();
    const [isError, setIsError] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        async function getAssessment() {
            try {
                const session = sessionStorage.getItem("ll__session");
                const sessionObj = JSON.parse(session || "false");
                if (!sessionObj) return;

                const grammarId = sessionObj.assessments.find(test => test.type.toLowerCase() === testType)?.id;
                if (!grammarId) return;

                setIsLoading(true)
                const enpoint = testType === "grammar" ? Endpoints.Grammar : Endpoints.Listening;
                const response = await fetch(`${enpoint}${grammarId}`, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                        "ngrok-skip-browser-warning": "true"
                    }
                });
                const data = await response.json() as AssessmentTest
                setData(data);
                setIsLoading(false)
            } catch (error) {
                console.error(error);
                setIsError(true)
            }
        }

        getAssessment();
    }, [])

    useEffect(() => {
        if (!data) return;
        data.currentQuestion = 0;
        data.testType = testType;

        if (data.testType === TestTypes.Listening) {
            data.passedWarning = false;
        }
        setAssessment(data)
    }, [data])

    if (isLoading) {
        return (
            <div style={{ margin: "auto" }}>
                <Loader />
            </div>

        )
    }
    if (isError) {
        // router.push("/assessment/introduction");
        return (
            <Alert
                message="Error Loading"
                description="Error occurred while loading the assessment. Try again!"
                type="error"
                showIcon
                style={{ width: "100%", height: "min-content" }}
            >
            </Alert>
        )
    }

    if (assessment) {
        return (
            <AssessmentProvider value={{ assessment, setAssessment }}>
                {
                    assessment.testType === TestTypes.Listening && !assessment.passedWarning ?
                        <ListeningPage /> :
                        <div style={{ display: 'flex', flexDirection: 'column', width: "100%" }}>
                            <QuestionCounter />
                            <QuestionProgress />
                            <Question />
                            <QuestionControl />
                        </div>
                }

            </AssessmentProvider>
        )
    }
}