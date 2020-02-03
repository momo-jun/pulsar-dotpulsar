/**
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

﻿using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;

namespace DotPulsar
{
    public sealed class Message
    {
        private readonly List<Internal.PulsarApi.KeyValue> _keyVaues;
        private IReadOnlyDictionary<string, string>? _properties;

        internal Message(
            MessageId messageId,
            Internal.PulsarApi.MessageMetadata metadata,
            Internal.PulsarApi.SingleMessageMetadata? singleMetadata,
            ReadOnlySequence<byte> data)
        {
            MessageId = messageId;
            ProducerName = metadata.ProducerName;
            PublishTime = metadata.PublishTime;
            Data = data;

            if (singleMetadata is null)
            {
                EventTime = metadata.EventTime;
                HasBase64EncodedKey = metadata.PartitionKeyB64Encoded;
                Key = metadata.PartitionKey;
                SequenceId = metadata.SequenceId;
                OrderingKey = metadata.OrderingKey;
                _keyVaues = metadata.Properties;
            }
            else
            {
                EventTime = singleMetadata.EventTime;
                HasBase64EncodedKey = singleMetadata.PartitionKeyB64Encoded;
                Key = singleMetadata.PartitionKey;
                OrderingKey = singleMetadata.OrderingKey;
                SequenceId = singleMetadata.SequenceId;
                _keyVaues = singleMetadata.Properties;
            }
        }

        public MessageId MessageId { get; }
        public ReadOnlySequence<byte> Data { get; }
        public string ProducerName { get; }
        public ulong SequenceId { get; }

        public bool HasEventTime => EventTime != 0;
        public ulong EventTime { get; }
        public DateTimeOffset EventTimeAsDateTimeOffset => DateTimeOffset.FromUnixTimeMilliseconds((long)EventTime);

        public bool HasBase64EncodedKey { get; }
        public bool HasKey => Key != null;
        public string? Key { get; }
        public byte[]? KeyBytes => HasBase64EncodedKey ? Convert.FromBase64String(Key) : null;

        public bool HasOrderingKey => OrderingKey != null;
        public byte[]? OrderingKey { get; }


        public ulong PublishTime { get; }
        public DateTimeOffset PublishTimeAsDateTimeOffset => DateTimeOffset.FromUnixTimeMilliseconds((long)PublishTime);

        public IReadOnlyDictionary<string, string> Properties => _properties ??= _keyVaues.ToDictionary(p => p.Key, p => p.Value);
    }
}
