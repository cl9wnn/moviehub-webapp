import React, { useEffect, useState, useMemo } from "react";
import TopicHeader from "../components/topics/TopicHeader.tsx";
import { getAllTopics } from "../services/topics/getAllTopics.ts";
import {type ListDiscussionTopicResponse, TAGS} from "../models/topic.ts";
import Header from "../components/header/Header.tsx";
import PageWrapper from "../components/common/PageWrapper.tsx";
import { useNavigate } from "react-router-dom";

const DiscussionListPage: React.FC = () => {
  const navigate = useNavigate();
  const [topics, setTopics] = useState<ListDiscussionTopicResponse[]>([]);
  const [searchQuery, setSearchQuery] = useState("");
  const [sortOption, setSortOption] = useState<"date" | "views">("date");
  const [selectedTag, setSelectedTag] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchTopics = async () => {
      try {
        const data = await getAllTopics();
        setTopics(data);
      } catch (err) {
        setError((err as Error).message);
      } finally {
        setLoading(false);
      }
    };

    fetchTopics();
  }, []);

  const filteredTopics = useMemo(() => {
    let filtered = topics.filter((topic) =>
      topic.title.toLowerCase().includes(searchQuery.toLowerCase())
    );

    if (selectedTag) {
      filtered = filtered.filter((topic) => topic.tags.includes(selectedTag));
    }

    if (sortOption === "date") {
      filtered = filtered.sort(
        (a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
      );
    } else if (sortOption === "views") {
      filtered = filtered.sort((a, b) => b.views - a.views);
    }

    return filtered;
  }, [topics, searchQuery, sortOption, selectedTag]);

  return (
    <PageWrapper backgroundClass="bg-[#d8d8d8]">
      <Header />
      <div className="max-w-4xl mx-auto px-4 py-8">

        <div className="flex items-center mb-6">
          <h1 className="text-3xl font-bold mr-3">Обсуждения</h1>
          <button
            onClick={() => navigate("/create-topic")}
            className="w-9 h-9 rounded-full bg-white border border-gray-300 shadow hover:bg-gray-100 flex items-center justify-center transition-colors"
            title="Создать обсуждение"
          >
            <span className="text-2xl leading-none translate-y-[-2px]">+</span>
          </button>
        </div>

        <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4 mb-6">
          <input
            type="text"
            placeholder="Поиск по названию..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="w-full md:w-1/2 p-2 border border-gray-300 rounded-lg"
          />

          <div className="flex gap-4">
            <select
              value={sortOption}
              onChange={(e) => setSortOption(e.target.value as "date" | "views")}
              className="p-2 border border-gray-300 rounded-lg"
            >
              <option value="date">Сначала новые</option>
              <option value="views">Сначала популярные</option>
            </select>

            <select
              value={selectedTag || ""}
              onChange={(e) =>
                setSelectedTag(e.target.value === "" ? null : e.target.value)
              }
              className="p-2 border border-gray-300 rounded-lg"
            >
              <option value="">Все теги</option>
              {TAGS.map((tag) => (
                <option key={tag} value={tag}>
                  {tag}
                </option>
              ))}
            </select>
          </div>
        </div>

        {loading ? (
          <p>Загрузка...</p>
        ) : error ? (
          <p className="text-red-500">{error}</p>
        ) : filteredTopics.length === 0 ? (
          <p>Темы не найдены.</p>
        ) : (
          filteredTopics.map((topic) => (
            <div
              key={topic.id}
              onClick={() => navigate(`/topics/${topic.id}`)}
              className="cursor-pointer hover:shadow-lg transition-shadow duration-200 rounded-xl"
            >
              <TopicHeader topic={topic}/>
            </div>
          ))
        )}
      </div>
    </PageWrapper>
  );
};

export default DiscussionListPage;
