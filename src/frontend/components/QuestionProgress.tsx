import { useAssessmentContext } from "@/context/assessment";
import { Progress } from "antd";
import "./styles/_progress.css"
import { TestTypes } from "@/constants";

export default function QuestionProgress() {
    const context = useAssessmentContext();
    if (!context) return;

    const { assessment } = context;

    const percent = (assessment.currentQuestion + 1) / assessment.questions.length * 100

    return (
        <div>
            <Progress type="line" trailColor="#183b0c" strokeColor={assessment.testType === TestTypes.Listening ? "#3155FF" : "#5AFB7A"} percent={parseFloat(percent.toFixed(0))} />
        </div>
    )
}