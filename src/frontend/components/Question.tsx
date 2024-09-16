'use client'
import { useAssessmentContext } from "@/context/assessment"
import GrammarQuestion from "./GrammarQuestion";
import { TestTypes } from "@/constants";

export default function Question() {
    const context = useAssessmentContext();
    if (!context) throw new Error("Test not loaded");

    const currentTest = context.assessment
    const { testType, currentQuestion: activeIndex, questions } = currentTest;
    const currentQuestionObject = questions[activeIndex];

    if (!currentQuestionObject) return;

    return <GrammarQuestion question={currentQuestionObject} />

    return <div>Question</div>
}