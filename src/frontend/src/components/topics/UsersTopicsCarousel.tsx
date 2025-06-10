import React from "react";
import type { UserDiscussionTopicResponse } from "../../models/topic.ts";
import UsersTopicCard from "./UsersTopicCard.tsx";
import Carousel from "../common/Carousel";
import TagButton from "../common/TagButton.tsx";

interface MovieTopicsCarouselProps {
  topics: UserDiscussionTopicResponse[];
  title: string;
  onCreateTopic: () => void;
}

const MovieTopicsCarousel: React.FC<MovieTopicsCarouselProps> = ({ topics, title, onCreateTopic }) => {
  if (topics.length === 0) return null;

  return (
    <div>
      <Carousel title={title}>
        {topics.map((topic) => (
          <UsersTopicCard key={topic.id} topic={topic}/>
        ))}
      </Carousel>
      <div className="mt-6">
        <TagButton onClick={onCreateTopic}>Создать обсуждение</TagButton>
      </div>
    </div>

  );
};

export default MovieTopicsCarousel;
