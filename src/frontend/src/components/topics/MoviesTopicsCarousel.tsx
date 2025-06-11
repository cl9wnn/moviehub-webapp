import React from "react";
import type { MovieDiscussionTopicResponse } from "../../models/topic.ts";
import Carousel from "../common/Carousel";
import TopicCard from "./MoviesTopicCard.tsx";
import TagButton from "../common/TagButton.tsx";

interface MovieTopicsCarouselProps {
  topics: MovieDiscussionTopicResponse[];
  onCreateTopic?: () => void;
}

const MovieTopicsCarousel: React.FC<MovieTopicsCarouselProps> = ({
                                                                   topics,
                                                                   onCreateTopic
                                                                 }) => {
  return (
    <div>
      {topics.length === 0 ? (
        <div className="py-4 text-center">
          <p className="text-gray-500 text-lg">По этому фильму не создано ни одно обсуждение. Стань первым!</p>
        </div>
      ) : (
        <Carousel>
          {topics.map((topic) => (
            <TopicCard key={topic.id} topic={topic}/>
          ))}
        </Carousel>
      )}

      <div className="mt-6 flex justify-center">
        <TagButton onClick={onCreateTopic}>Создать обсуждение</TagButton>
      </div>
    </div>
  );
};

export default MovieTopicsCarousel;